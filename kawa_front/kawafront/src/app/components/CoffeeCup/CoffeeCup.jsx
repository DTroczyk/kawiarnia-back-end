import React from "react";
import "./CoffeeCup.scss";
function CoffeeCup() {
  return (
    <div className="wrapper">
      <div className="coffeeCup">
            <div className="coffeeCup__fill--coffee">1</div>
            <div className="coffeeCup__fill">2</div>
            <div className="coffeeCup__fill">3</div>
            <div className="coffeeCup__fill">3</div>
            <div className="coffeeCup__fill">3</div>
            <div className="coffeeCup__fill">3</div>
            <div className="coffeeCup__fill">3</div>
            <div className="coffeeCup__fill">3</div>
            <div className="coffeeCup__fill">3</div>
            <div className="coffeeCup__fill">3</div>



      </div>
      <div className="coffeeCup__ear"></div>
     { /* Nie podpięte trzeba przedyskutować trzymanie stanu i ich wygląd*/}
      <button>Dodaj Mleka</button>
      <button>Dodaj Kawy</button>
      <button>Dodaj Czekolady</button>
    </div>
  );
}
export default CoffeeCup;
