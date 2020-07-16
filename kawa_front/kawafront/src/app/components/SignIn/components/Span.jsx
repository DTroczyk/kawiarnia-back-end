import React from "react";

const Span = ({ children, type }) => (
  <span className={`signIn__input--${type}`}>{children}</span>
);
export default Span;
