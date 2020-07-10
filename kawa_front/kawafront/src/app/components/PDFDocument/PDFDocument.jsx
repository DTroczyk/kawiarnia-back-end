import React from "react";

import {
  Page,
  Text,
  View,
  Image,
  Document,
  StyleSheet,
  PDFViewer,
} from "@react-pdf/renderer";

// Create styles
const styles = StyleSheet.create({
  page: {
    flexDirection: "column",
    backgroundColor: "#E4E4E4",
  },
  section: {
    margin: 1,
    padding: 1,
    flexGrow: 1,
  },
});

// Create Document Component
function PDFDocumment({ fullName }) {
  return (
    <Document>
      <Page size="A4" style={styles.page}>
        <View style={styles.section}>
          <Image src="https://image.freepik.com/free-vector/logo-template-coffee-business-design_23-2148512021.jpg"></Image>
          <Text>Zamawiający: {fullName}</Text>
          <Text>Zamawiający: {fullName}</Text>
        </View>
      </Page>
    </Document>
  );
}

export default PDFDocumment;
