import React from "react";
import "./Pay.scss";
import { Wrapper } from "./components";
import PDFDocument from "../PDFDocument/PDFDocument";
import { PDFViewer } from "@react-pdf/renderer";
import Map from "../Map/Map";
import { useSelector } from "react-redux";
import Payment from "../Payment/Payment";
import Delivery from "../Delivery/Delivery";

function Pay() {
  const user = useSelector((state) => state.user.data);
  const items = useSelector((state) =>
    state.bucket.bucketItems.filter((item) => item.isSelectedToPay === true)
  );
  return (
    <Wrapper>
      <PDFViewer width="60%" height={500}>
        <PDFDocument user={user} items={items} />
      </PDFViewer>
      <Delivery />
      <Payment shouldRenderPaymentMethods={true} isAddToBucketVisible={false} />
    </Wrapper>
  );
}
export default Pay;
