import React, { Fragment } from "react";
import "./Bucket.scss";
import { useSelector } from "react-redux";

import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import { Block, Button, Field, Info, Text, Wrapper } from "./components";
function getTotalValue(itemsArr) {
  let acc = 0;
  itemsArr.map((item) => (acc += item.price));
  return Math.round(acc * Math.pow(10, 2)) / Math.pow(10, 2);
}
function getCountOfSelectedItemToPay(itemsArr) {
  let acc = 0;
  itemsArr.map((item) => {
    acc += item.isSelectedToPay ? 1 : 0;
    return null;
  });
  return acc;
}
function Bucket() {
  const bucketItems = useSelector((state) => state.bucket.bucketItems);

  return (
    <Wrapper>
      <Info>
        <Text>Wartość Twojego koszyka: {getTotalValue(bucketItems)}zł</Text>

        {getCountOfSelectedItemToPay(bucketItems) > 0 ? (
          <Button name="pay">Opłać zaznaczone produkty</Button>
        ) : null}
      </Info>
      <Block>
        {bucketItems.length
          ? bucketItems.map((item, idx) => (
              <Field
                key={idx}
                idx={idx}
                coffeeName={item.coffeeName}
                className={
                  item.isSelectedToPay
                    ? "bucket__field selected"
                    : "bucket__field"
                }
              >
                <Text type="name">{item.coffeeName}</Text>
                {!item.isSelectedToPay ? (
                  <Fragment>
                    <Button idx={idx} name="delete">
                      &#128465;
                    </Button>
                  </Fragment>
                ) : null}
              </Field>
            ))
          : "Brak zamowień"}
      </Block>
      <ToastContainer
        position="top-left"
        autoClose={5000}
        hideProgressBar
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </Wrapper>
  );
}
export default Bucket;
