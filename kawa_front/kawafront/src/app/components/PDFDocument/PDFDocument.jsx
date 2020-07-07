import React from 'react';
import { Page, Text, View, Document, StyleSheet } from '@react-pdf/renderer';

// Create styles
const styles = StyleSheet.create({
  page: {
    flexDirection: 'column',
    backgroundColor: '#E4E4E4'
  },
  section: {
    margin: 1,
    padding: 1,
    flexGrow: 1
  }
});

// Create Document Component
export default () => (
  <Document>
    <Page size="A4" style={styles.page}>
      <View style={styles.section}>
        <Text>Zamawiający: XYZ</Text>
      </View>
      <View style={styles.section}>
        <Text>KAWA: TYP: LATTE</Text>
      </View>
    </Page>
  </Document>
);