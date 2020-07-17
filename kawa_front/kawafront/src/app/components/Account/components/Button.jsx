import React from "react";

const Button = ({ children }) => {
  function handleClick() {}
  return (
    <button onClick={handleClick} className="account__button">
      {children}
    </button>
  );
};
export default Button;
