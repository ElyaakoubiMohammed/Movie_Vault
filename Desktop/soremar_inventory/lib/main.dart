import 'package:flutter/material.dart';
import 'package:soremar_inventory/screens/home_page.dart';
import 'package:soremar_inventory/screens/error_page.dart';

void main() => runApp(App());

class App extends StatelessWidget {
  const App({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      initialRoute: HomePage.path,
      routes: {
        HomePage.path: (context) => HomePage(),
        ErrorPage.path: (context) => const ErrorPage(
            errorMessage: 'NFC is not available on this device'),
      },
    );
  }
}
