import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

class Calling extends StatefulWidget {
  const Calling({Key key}) : super(key: key);

  @override
  _CallingState createState() => _CallingState();
}

class _CallingState extends State<Calling> {
  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Container(
        color: Colors.white,
        width: MediaQuery.of(context).size.width,
        height: MediaQuery.of(context).size.height,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Center(child:  Container(
              width: 250.0,
              height: 250.0,
              decoration: BoxDecoration(image: DecorationImage(image: AssetImage('images/efectcall.png'))),
              alignment: Alignment.center, // where to position the child
              child: CircleAvatar(
                backgroundImage: AssetImage('images/angelina1.png'),radius: 45.0,
              )
            ),),

            Padding(padding: EdgeInsets.only(top: 30),child: Center(child:Text('Name User',style: GoogleFonts.nunitoSans(
              fontSize: 16,color: Colors.black,
            ),)),),
            Padding(padding: EdgeInsets.only(top: 10),child: Center(child:Text('Ringing...',style: GoogleFonts.nunitoSans(
              fontSize: 12,color: Colors.grey.shade600,
            ),)),),
            Padding(padding: EdgeInsets.only(top: 150,right: 40,left: 40,bottom: 40),child:Stack(
              children: <Widget>[
                Container(
                  margin: const EdgeInsets.only(top: 20.0),
                  height: 70,width: 276,
                  decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(100.0),border: Border.all(width: 1.0,color: Color(0xFF29ABE2))),
                  child: Row(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: <Widget>[
                   Padding( padding: EdgeInsets.only(left: 40),child: GestureDetector(child: Image.asset('images/mic.png',height: 30,width: 30,),),),
                   Padding( padding: EdgeInsets.only(right: 40),child: GestureDetector(child: Image.asset('images/volume.png',height: 30,width: 30,),),)


                  ],),
                ),
                Positioned(
                  top: .0,
                  left: .0,
                  right: .0,
                  child: GestureDetector(
                    onTap: (){Navigator.pop(context);},
                    child: Center(
                      child: CircleAvatar(
                          radius: 30.0,
                          backgroundColor: Color(0xFFFF2C00),
                          child: Padding(padding: EdgeInsets.only(right: 3,top: 3),child: Image.asset('images/call.png',width: 30,height: 30,fit:BoxFit.contain,),)
                      ),
                    ),
                  )
                )
              ],
            ),)
          ],
        )
      ),
    );
  }
}
