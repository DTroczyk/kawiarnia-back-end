import React from "react";

const Span = ({ children, type }) => (
  <span className={`login__input--${type}`}></span>
);
export default Span;
