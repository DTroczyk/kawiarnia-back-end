import React from "react";

const Button = ({ id,children }) => {
  function handleClick() {

    //fetch to API
  }
  return (
    <button onClick={handleClick} className="deleteItemFromBucket__button">
      {children}
    </button>
  );
};
export default Button;
