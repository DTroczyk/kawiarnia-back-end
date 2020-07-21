import React,{Fragment} from "react";
import {
  Page,
  Text,
  View,

  Document,
  StyleSheet,
  
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
function getTotalPrice(itemsArr) {
  let price = 0;
  itemsArr.map((item) => (price += item.price));
  return price;
}
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
          {items.map((item,idx) => (
            <Fragment key={idx}>
              <Text>
                {item.coffeeName}:
                {new Intl.NumberFormat(window.navigator.language, {
                  style: "currency",
                  currency: "PLN",
                }).format(item.price)}
              </Text>
            </Fragment>
          ))}
          <Text>
            Suma:
            {new Intl.NumberFormat(window.navigator.language, {
              style: "currency",
              currency: "PLN",
            }).format(getTotalPrice(items))}
          </Text>
        </View>
      </Page>
    </Document>
  );
}

export default PDFDocument;
