import React from "react";
import "./Account.scss";
import { useSelector, useDispatch } from "react-redux";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useHistory } from 'react-router-dom'
import userActions from "../../redux/user/actions";
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
        <div className="account__field">
          <input
            id="username"
            type="text"
            className="account__input"
            value={username}
            onChange={handleChange}
            required
          />
          <span className="account__input-highlight"></span>
          <span className="account__input-bar"></span>
          <label htmlFor="username">Login</label>
        </div>
        <div className="account__field">
          <input
            id="password"
            type="password"
            className="account__input"
            value={password}
            onChange={handleChange}
            required
          />
          <span className="account__input-highlight"></span>
          <span className="account__input-bar"></span>
          <label htmlFor="password">Hasło</label>
        </div>
        <div className="account__field">
          <input
            id="email"
            type="text"
            className="account__input"
            value={email}
            onChange={handleChange}
            required
          />
          <span className="account__input-highlight"></span>
          <span className="account__input-bar"></span>
          <label htmlFor="email">email</label>
        </div>
        <div className="account__field">
          <input
            id="telephone"
            type="text"
            className="account__input"
            value={telephone}
            onChange={handleChange}
            required
          />
          <span className="account__input-highlight"></span>
          <span className="account__input-bar"></span>
          <label htmlFor="telephone">Nr telefonu</label>
        </div>
        <h1>Dane osobowe</h1>
        <div className="account__field">
          <input
            id="adress"
            type="text"
            className="account__input"
            value={firstName}
            onChange={handleChange}
            required
          />
          <span className="account__input-highlight"></span>
          <span className="account__input-bar"></span>
          <label htmlFor="adress">Imię</label>
        </div>
        <div className="account__field">
          <input
            id="name"
            type="text"
            className="account__input"
            value={lastName}
            onChange={handleChange}
            required
          />
          <span className="account__input-highlight"></span>
          <span className="account__input-bar"></span>
          <label htmlFor="name">Nazwisko</label>
        </div>
        <div className="account__field">
          <label htmlFor="">Data urodzenia </label>
          <div>
            <DatePicker
              selected={new Date(dateOfBirth)}
              onChange={(date) => {
                handleChange({ target: { id: "dateOfBirth", value: date } });
              }}
            />
          </div>
        </div>
        <h1>Adres</h1>
        <div className="account__field">
          <input
            id="houseNumber"
            type="text"
            className="account__input"
            value={houseNumber}
            onChange={handleChange}
            required
          />
          <span className="account__input-highlight"></span>
          <span className="account__input-bar"></span>
          <label htmlFor="houseNumber">Numer domu/lokalu</label>
        </div>
        <div className="account__field">
          <input
            id="road"
            type="text"
            className="account__input"
            value={road}
            onChange={handleChange}
            required
          />
          <label htmlFor="road">Ulica</label>
        </div>

        <div className="account__field">
          <input
            id="zipcode"
            type="text"
            className="account__input"
            value={zipcode}
            onChange={handleChange}
            required
          />
          <span className="account__input-highlight"></span>
          <span className="account__input-bar"></span>
          <label htmlFor="zipcode">Kod pocztowy</label>
        </div>
        <div className="account__field">
          <input
            id="place"
            type="text"
            className="account__input"
            value={place}
            onChange={handleChange}
            required
          />
          <span className="account__input-highlight"></span>
          <span className="account__input-bar"></span>
          <label htmlFor="place">Miejscowość</label>
        </div>
        <button className="account__button" onClick={handleClick}>Zapisz</button>
      </div>
    </div>
  );
}
export default Account;
