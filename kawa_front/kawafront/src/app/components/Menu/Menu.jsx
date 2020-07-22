import React from "react";
import "./Menu.scss";
import {Wrapper,Block} from './components'
const Menu = () => (
  <Wrapper>
    <Block name="history">Historia</Block>
    <Block name="bucket">Koszyk</Block>
    <Block name="account">Konto</Block>
    <Block name="newOrder">Nowe zamówienie</Block>
    <Block name="logout">Wyloguj</Block>
  </Wrapper>
);

export default Menu;
