import React from "react";

const Text = ({ children,type="text" }) => <p className={`bucket__${type}`}>{children}</p>;
export default Text;
