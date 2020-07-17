import React from "react";
import { Wrapper } from "./components";
import PDFDocument from "../PDFDocument/PDFDocument";
import { PDFViewer } from "@react-pdf/renderer";
import "./Pay.scss";
import { useSelector } from "react-redux";
function Pay() {
  const user = useSelector((state) => state.user.data);
  const items = useSelector((state) =>
    state.bucket.bucketItems.filter((item) => item.isSelectedToPay === true)
  );
  return (
    <Wrapper>
      <PDFViewer width="100%" height="100%">
        <PDFDocument user={user} items={items} />
      </PDFViewer>
    </Wrapper>
  );
}
export default Pay;
