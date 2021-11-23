import 'package:flutter/material.dart';
import 'package:flutter/painting.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:intl_phone_number_input/intl_phone_number_input.dart';
import 'package:localphone/call/calling.dart';
import 'package:numeric_keyboard/numeric_keyboard.dart';
import 'package:sliding_up_panel/sliding_up_panel.dart';

class Call extends StatefulWidget {
  const Call({Key key}) : super(key: key);

  @override
  _CallState createState() => _CallState();
}

class _CallState extends State<Call> {

  final TextEditingController controller = TextEditingController();
  PhoneNumber number = PhoneNumber(isoCode: 'BR');

  void getPhoneNumber(String phoneNumber) async {
    PhoneNumber number =
    await PhoneNumber.getRegionInfoFromPhoneNumber(phoneNumber, 'BR');

    setState(() {
      this.number = number;
    });
  }

  _onKeyboardTap(String value) {
    setState(() {
      controller.text = controller.text + value;
    });
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      floatingActionButton: FloatingActionButton(
        elevation: 10,
        child: Container(
          width: 60,
          height: 60,
          padding: EdgeInsets.only(top: 15,right: 15,left: 10,bottom: 10),
          child: Image.asset('images/call.png'),
          decoration: BoxDecoration(
              shape: BoxShape.circle,
              gradient: LinearGradient(colors: [Color(0xFF29ABE2), Color(0xFF68D2FF)])
          ),
        ),
        onPressed: () {Navigator.push(context, MaterialPageRoute(builder: (context)=> Calling()));},
      ),
      floatingActionButtonLocation: FloatingActionButtonLocation.centerFloat,
      appBar: AppBar(
        elevation: 0,
        iconTheme: IconThemeData(color: Colors.black),
        backgroundColor: Colors.white,
        title: Padding(padding:EdgeInsets.only(top: 30),child: Text('Call',style: GoogleFonts.nunitoSans(fontSize: 12,color: Colors.black,fontWeight: FontWeight.bold),),),
          centerTitle: true,
      ),
      body: Stack(
        fit: StackFit.expand,
        children:<Widget> [
          FractionallySizedBox(
            alignment: Alignment.topCenter,
            heightFactor: 0.8,
            child: Container(
              color: Colors.white60,
            ),
          ),
          FractionallySizedBox(
            alignment: Alignment.bottomCenter,
            heightFactor: 0.3,
            child: Container(
              color: Colors.transparent,
            ),
          ),
          SlidingUpPanel(
            borderRadius: BorderRadius.only(topRight: Radius.circular(15),topLeft: Radius.circular(15)),
            minHeight: 470,
            maxHeight: MediaQuery.of(context).size.height * 0.80,
            body: Container(color: Colors.transparent,
            ),
            panelBuilder: (ScrollController controller) => _painelBody(controller),
          ),

        ],

      ),
    );
  }

  SingleChildScrollView _painelBody(ScrollController controller) {
    double hPadding = 5;

    return SingleChildScrollView(
      controller: controller,
      physics: ClampingScrollPhysics(),
      child: Column(
        children:<Widget> [
          Container(
            padding: EdgeInsets.symmetric(horizontal: hPadding),
            height: double.maxFinite,
            child:Column(
              mainAxisSize: MainAxisSize.max,
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children:<Widget> [
                titleSection(),
                Expanded(child: actionSection())

              ],
            ),
          ),

        ],
      ),
    );
  }

  Container actionSection() {
    return Container(
      padding: EdgeInsets.zero,
      height: double.maxFinite,
      width: MediaQuery.of(context).size.width,
      child: NumericKeyboard(
          onKeyboardTap: _onKeyboardTap,
          textColor: Colors.black,
          rightButtonFn: () {
            setState(() {
              controller.text = controller.text.substring(0, controller.text.length - 1);
            });
          },
          rightIcon: Icon(Icons.arrow_back_ios, color: Colors.black,),
          leftButtonFn: () {
            print('left button clicked');
          },
          leftIcon: Icon(Icons.check, color: Colors.black,),
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
      )
    );

  }

  Column _infoCell({@required String title, @required String value}){
    return Column(
      children:<Widget> [
        Text(
          title,
          style: TextStyle(
            fontWeight: FontWeight.w700,
            fontSize: 14,
          ),
        ),
        SizedBox(
          height: 8,
        ),
        Text(
          value,
          style: TextStyle(
            fontWeight: FontWeight.w700,
            fontSize: 14,
          ),
        )


      ],

    );

  }

  Column titleSection() {
    return Column(
      children:<Widget> [
        Padding(padding: EdgeInsets.only(top: 20)),
        Padding(
          padding: EdgeInsets.only(top: 10,left: 20,right: 20),
          child: Container(
            width: 496, height: 70,
            child: InternationalPhoneNumberInput(
              onInputChanged: (PhoneNumber number){
                print(number.phoneNumber);
              },
              onInputValidated: (bool value){
                print(value);
              },
              selectorConfig: SelectorConfig(
                selectorType: PhoneInputSelectorType.DROPDOWN,
              ),
              ignoreBlank: false,
              inputDecoration: InputDecoration(hintText: 'Write number here',hintStyle: GoogleFonts.inter(fontSize: 16),border: InputBorder.none),
              autoValidateMode: AutovalidateMode.disabled,
              selectorTextStyle: TextStyle(color: Colors.black),
              initialValue: number,
              textFieldController: controller,
              formatInput: false,
              keyboardType:
              TextInputType.none,
              inputBorder: OutlineInputBorder(),
              onSaved: (PhoneNumber number) {
                print('On Saved: $number');
              },
            ),
          ),
        ),

      ],

    );
  }
}
