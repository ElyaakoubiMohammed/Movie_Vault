import 'package:flutter/material.dart';
import '../services/nfc_service.dart';

class HomePage extends StatelessWidget {
  HomePage({super.key});
  static const path = "/";

  final NfcService _nfcService = NfcService();

  void _onReadNFC(BuildContext context) async {
    try {
      List<String> data = await _nfcService.readNfcTag(context);
      if (data.isNotEmpty) {
        print("NFC Data: $data");
        showDialog(
          context: context,
          builder: (context) => AlertDialog(
            title: Text('NFC Data'),
            content: Column(
              mainAxisSize: MainAxisSize.min,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: data.map((payload) => Text(payload)).toList(),
            ),
            actions: [
              TextButton(
                onPressed: () => Navigator.of(context).pop(),
                child: Text('OK'),
              ),
            ],
          ),
        );
      } else {
        print("No data found on NFC tag.");
      }
    } catch (e) {
      print("Error reading NFC: $e");
      showDialog(
        context: context,
        builder: (context) => AlertDialog(
          title: Text('Error'),
          content: Text('Error reading NFC: $e'),
          actions: [
            TextButton(
              onPressed: () => Navigator.of(context).pop(),
              child: Text('OK'),
            ),
          ],
        ),
      );
    }
  }

  void _onWriteNFC(BuildContext context) async {
    try {
      await _nfcService.writeNfcTag("ikhooo");
      print("Data written to NFC tag.");
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Data written to NFC tag.')),
      );
    } catch (e) {
      print("Error writing NFC: $e");
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Error writing to NFC tag: $e')),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: [
            ElevatedButton(
              onPressed: () => _onReadNFC(context),
              child: const Text("Read NFC"),
            ),
            ElevatedButton(
              onPressed: () => _onWriteNFC(context),
              child: const Text("Write NFC"),
            ),
          ],
        ),
      ),
    );
  }
}
