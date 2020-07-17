import React, { Fragment } from "react";
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
function PDFDocument({ user, items }) {
  return (
    <Document>
      <Page size="A4" style={styles.page}>
        <View style={styles.section}>
          <Text>Super Kawiarnia XYZ</Text>
          <Text>
            Zamawiający: {user.username} {user.firstName} {user.lastName}
          </Text>
          <Text>{user.email}</Text>
        </View>
        <View style={styles.section}>
          <Text>Zamówienie:</Text>
          {items.map((item) => (
            <>
              <Text>{item.coffeeName}</Text>
              <Text>
                {new Intl.NumberFormat(window.navigator.language, {
                  style: "currency",
                  currency: "PLN",
                }).format(item.price)}
              </Text>
            </>
          ))}
        </View>
      </Page>
    </Document>
  );
}

export default PDFDocument;
