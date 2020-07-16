import React from "react";

const Span = ({ children, type }) => (
  <span className={`account__input--${type}`}>{children}</span>
);
export default Span;
