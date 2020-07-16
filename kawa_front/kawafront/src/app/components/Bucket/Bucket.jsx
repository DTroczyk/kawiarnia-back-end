import React, { useState } from "react";
import "./Bucket.scss";
import { useDispatch, useSelector } from "react-redux";
import bucketActions from "../../redux/bucket/actions";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import { Block, Button, Field, Info, Text, Wrapper } from "./components";
function getTotalValue(itemsArr) {
  let acc = 0;
  itemsArr.map((item) => (acc += item.price));
  return Math.round(acc * Math.pow(10, 2)) / Math.pow(10, 2);
}

function Bucket() {
  const bucketItems = useSelector((state) => state.bucket.bucketItems);
  const dispatch = useDispatch();
  const [selectedItem, setSelectedItem] = useState(null);
  function handleDelete(idx) {
   
  }
  return (
    <Wrapper>
      <Info>
        <Text>Wartość Twojego koszyka: {getTotalValue(bucketItems)}zł</Text>
        <Text>{selectedItem?.coffeeName}</Text>
      </Info>
      <Block>
        {bucketItems.length
          ? bucketItems.map((item, idx) => (
              <Field
                key={idx}
                coffeeName={item.coffeeName}
                className="bucket__field"
              >
                <Text type="name">{item.coffeeName}</Text>
                <Button idx={idx} name="pay">Opłać zamówienie</Button>
                <Button idx={idx} name="delete">&#128465;</Button>
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
