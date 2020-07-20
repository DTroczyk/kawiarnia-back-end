import React from "react";
import "./Payment.scss";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { Block, Wrapper, P, Span } from "./components";
import { Fragment } from "react";
const Payment = ({
  isAddToBucketVisible = true,
  shouldRenderPaymentMethods = false,
}) => (
  <Wrapper>
    {shouldRenderPaymentMethods ? (
      <Fragment>
        <Block>
          <Span>Blik</Span>
          <P>11.30z</P>
        </Block>
        <Block>
          <Span>Płatność przy odbiorze</Span>
          <P>11.30</P>
        </Block>
      </Fragment>
    ) : (
      <Block type='payNow'>
        <Span>Zapłać teraz!</Span>
      </Block>
    )}

    {isAddToBucketVisible ? (
      <Block type="addToBucket">
        <Span>Albo</Span>
        <P>Dodaj do koszyka</P>
      </Block>
    ) : null}
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
export default Payment;
