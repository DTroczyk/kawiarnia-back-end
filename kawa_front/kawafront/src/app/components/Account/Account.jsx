import React from "react";
import "./Account.scss";
import { useSelector, useDispatch } from "react-redux";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useHistory } from 'react-router-dom'
import userActions from "../../redux/user/actions";
import {
  Field,
  Span,
  Label
} from "./components";

function Account() {
  const user = useSelector((state) => state.user.data);
  const {
    username,
    password,
    email,
    firstName,
    lastName,
    road,
    zipcode,
    place,
    telephone,
    houseNumber,
    dateOfBirth,
  } = user;
  const history = useHistory();
  function handleChange(event) {
    const { id: name, value } = event.target;

    switch (name) {
      case "username":
        dispatch(userActions.changeInputValue({ name, value }));
        break;
      case "password":
        dispatch(userActions.changeInputValue({ name, value }));
        break;
      case "email":
        dispatch(userActions.changeInputValue({ name, value }));
        break;
      case "telephone":
        dispatch(userActions.changeInputValue({ name, value }));
        break;
      case "firstName":
        dispatch(userActions.changeInputValue({ name, value }));
        break;
      case "lastName":
        dispatch(userActions.changeInputValue({ name, value }));
        break;
      case "houseNumber":
        dispatch(userActions.changeInputValue({ name, value }));
        break;
      case "dateOfBirth":
        dispatch(userActions.changeInputValue({ name, value }));
        break;
      case "road":
        dispatch(userActions.changeInputValue({ name, value }));
        break;
      case "zipcode":
        dispatch(userActions.changeInputValue({ name, value }));
        break;
      case "place":
        dispatch(userActions.changeInputValue({ name, value }));
        break;
    }
  }
  function handleClick() {
    //fetch to API and redirect

    history.push('/panel')
  }

  const dispatch = useDispatch();
  return (
    <div className="account">
      <div className="account__container">
        <h1>Konto</h1>
        <Field>
          <input
            id="username"
            type="text"
            className="account__input"
            value={username}
            onChange={handleChange}
            required
          />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>LOGIN</Label>
        </Field>
        <Field>
          <input
            id="password"
            type="password"
            className="account__input"
            value={password}
            onChange={handleChange}
            required
          />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>HASŁO</Label>
        </Field>
        <Field>
          <input
            id="email"
            type="text"
            className="account__input"
            value={email}
            onChange={handleChange}
            required
          />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>E-MAIL</Label>
        </Field>
        <Field>
          <input
            id="telephone"
            type="text"
            className="account__input"
            value={telephone}
            onChange={handleChange}
            required
          />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>NR TELEFONU</Label>
        </Field>
        <h1>Dane osobowe</h1>
        <Field>
          <input
            id="adress"
            type="text"
            className="account__input"
            value={firstName}
            onChange={handleChange}
            required
          />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>IMIĘ</Label>
        </Field>
        <Field>
          <input
            id="name"
            type="text"
            className="account__input"
            value={lastName}
            onChange={handleChange}
            required
          />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>NAZWISKO</Label>
        </Field>
        <Field>
          <DatePicker
            className="signIn__input"
            selected={new Date(dateOfBirth)}
            onChange={(date) => {
              handleChange({ target: { id: "dateOfBirth", value: date } });
            }}
          />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>DATA URODZENIA</Label>
        </Field>
        <h1>Adres</h1>
        <Field>
          <input
            id="houseNumber"
            type="text"
            className="account__input"
            value={houseNumber}
            onChange={handleChange}
            required
          />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>NUMER DOMU/LOKALU</Label>
        </Field>
        <Field>
          <input
            id="road"
            type="text"
            className="account__input"
            value={road}
            onChange={handleChange}
            required
          />
          <Label>ULICA</Label>
        </Field>

        <Field>
          <input
            id="zipcode"
            type="text"
            className="account__input"
            value={zipcode}
            onChange={handleChange}
            required
          />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>KOD POCZTOWY</Label>
        </Field>
        <Field>
          <input
            id="place"
            type="text"
            className="account__input"
            value={place}
            onChange={handleChange}
            required
          />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>MIEJSCOWOŚĆ</Label>
        </Field>
        <button className="account__button" onClick={handleClick}>Zapisz</button>
      </div>
    </div>
  );
}
export default Account;
