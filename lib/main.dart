import 'package:flutter/material.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: LoginScreen(), // Show the login screen initially
    );
  }
}

class LoginScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      resizeToAvoidBottomInset: false, // Added to prevent resizing with keyboard
      body: SingleChildScrollView( // Wrap with SingleChildScrollView
        child: Container(
          height: MediaQuery.of(context).size.height,
          decoration: BoxDecoration(
            gradient: LinearGradient(
              begin: Alignment.topRight,
              end: Alignment.bottomLeft,
              colors: [Colors.purpleAccent, Colors.blueAccent],
            ),
          ),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              Expanded(
                flex: 1,
                child: Container(
                  padding: EdgeInsets.all(20.0),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      AnimatedText(text: 'Welcome Back', animationDelay: 2000),
                      AnimatedText(
                          text: 'Log in to begin scanning',
                          animationDelay: 2100),
                    ],
                  ),
                ),
              ),
              Expanded(
                flex: 2,
                child: Container(
                  padding: EdgeInsets.all(20.0),
                  decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.only(
                      topLeft: Radius.circular(30.0),
                      topRight: Radius.circular(30.0),
                    ),
                  ),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      TextFieldWidget(
                        hintText: 'Email Address',
                        icon: Icons.email,
                        animationDelay: 2200,
                      ),
                      SizedBox(height: 20.0),
                      TextFieldWidget(
                        hintText: 'Password',
                        isPassword: true,
                        icon: Icons.lock,
                        animationDelay: 2300,
                      ),
                      SizedBox(height: 15.0),
                      Align(
                        alignment: Alignment.centerRight,
                        child: Padding(
                          padding: const EdgeInsets.only(right: 10.0),
                          child: GestureDetector(
                            onTap: () {},
                            child: Text(
                              'Need an account?',
                              style: TextStyle(
                                color: Colors.black87,
                                fontSize: 14.0,
                              ),
                            ),
                          ),
                        ),
                      ),
                      SizedBox(height: 10.0),
                      SizedBox(height: 90.0),
                      ElevatedButton(
                        onPressed: () {
                          // Simulate successful login, navigate to main menu
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) => MainMenuScreen(),
                            ),
                          );
                        },
                        style: ElevatedButton.styleFrom(
                          foregroundColor: Colors.transparent, 
                          backgroundColor: Colors.transparent,
                          shadowColor: Colors.transparent,
                          elevation: 0,
                          padding: EdgeInsets.symmetric(vertical: 15.0),
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(40.0),
                          ),
                        ),
                        child: Ink(
                          decoration: BoxDecoration(
                            gradient: LinearGradient(
                              colors: [Colors.purpleAccent, Colors.blueAccent],
                            ),
                            borderRadius: BorderRadius.circular(40.0),
                          ),
                          child: Container(
                            constraints: BoxConstraints(maxWidth: 120),
                            alignment: Alignment.center,
                            child: AnimatedText(
                              text: 'LOGIN',
                              animationDelay: 2500,
                              color: Colors.white,
                            ),
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

class MainMenuScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Main Menu'),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            ElevatedButton(
              onPressed: () {
                // Navigate to Read Tag screen
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => ReadTagScreen()),
                );
              },
              child: Text('Read Tag'),
            ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: () {
                // Navigate to Write Tag screen
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => WriteTagScreen()),
                );
              },
              child: Text('Write Tag'),
            ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: () {
                // Navigate to Erase Tag screen
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => EraseTagScreen()),
                );
              },
              child: Text('Erase Tag'),
            ),
          ],
        ),
      ),
    );
  }
}

class ReadTagScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Read Tag'),
      ),
      body: Center(
        child: Text('Read Tag Screen'),
      ),
    );
  }
}

class WriteTagScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Write Tag'),
      ),
      body: Center(
        child: Text('Write Tag Screen'),
      ),
    );
  }
}

class EraseTagScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Erase Tag'),
      ),
      body: Center(
        child: Text('Erase Tag Screen'),
      ),
    );
  }
}

class AnimatedText extends StatelessWidget {
  final String text;
  final int animationDelay;
  final Color? color;

  const AnimatedText({
    required this.text,
    required this.animationDelay,
    this.color,
  });

  @override
  Widget build(BuildContext context) {
    return TweenAnimationBuilder(
      tween: Tween<double>(begin: 0, end: 1),
      duration: Duration(milliseconds: 400),
      builder: (_, double value, __) {
        return Opacity(
          opacity: value,
          child: Transform.translate(
            offset: Offset(0, (1 - value) * 20),
            child: Text(
              text,
              style: TextStyle(
                fontSize: 30.0,
                fontWeight: FontWeight.bold,
                color: color ?? Colors.white,
              ),
            ),
          ),
        );
      },
    );
  }
}

class TextFieldWidget extends StatelessWidget {
  final String hintText;
  final bool isPassword;
  final IconData? icon;
  final int animationDelay;

  const TextFieldWidget({
    required this.hintText,
    this.isPassword = false,
    this.icon,
    required this.animationDelay,
  });

  @override
  Widget build(BuildContext context) {
    return TweenAnimationBuilder(
      tween: Tween<double>(begin: 0, end: 1),
      duration: Duration(milliseconds: 400),
      builder: (_, double value, __) {
        return Opacity(
          opacity: value,
          child: Transform.translate(
            offset: Offset((1 - value) * 20, 0),
            child: TextField(
              obscureText: isPassword,
              style: TextStyle(fontSize: 18.0),
              decoration: InputDecoration(
                hintText: hintText,
                prefixIcon: Icon(icon),
                filled: true,
                fillColor: Colors.grey[200],
                border: OutlineInputBorder(
                  borderRadius: BorderRadius.circular(10.0),
                  borderSide: BorderSide.none,
                ),
                contentPadding: EdgeInsets.symmetric(
                  horizontal: 20.0,
                  vertical: 15.0,
                ),
              ),
            ),
          ),
        );
      },
    );
  }
}
