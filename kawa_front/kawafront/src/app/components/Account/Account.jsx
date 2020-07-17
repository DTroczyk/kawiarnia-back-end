import React from "react";
import "./Account.scss";
import { useSelector, useDispatch } from "react-redux";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import userActions from "../../redux/user/actions";
import { Field, Span, Label, Container, Text, Input,Wrapper,Button } from "./components";


function Account() {
  const user = useSelector((state) => state.user.data);
  const { dateOfBirth } = user;
  const dispatch = useDispatch();
  return (
    <Wrapper>
      <Container>
        <Text>Konto</Text>
        <Field>
          <Input name="username" />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>LOGIN</Label>
        </Field>
        <Field>
          <Input name="password" type="password" />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>HASŁO</Label>
        </Field>
        <Field>
          <Input name="email" />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>E-MAIL</Label>
        </Field>
        <Field>
          <Input name="telephone" />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>NR TELEFONU</Label>
        </Field>
        <Text>Dane osobowe</Text>
        <Field>
          <Input name="firstName" />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>IMIĘ</Label>
        </Field>
        <Field>
          <Input name="lastName" />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>NAZWISKO</Label>
        </Field>
        <Field>
          <DatePicker
            className="signIn__input"
            selected={new Date(dateOfBirth)}
            onChange={(date) => {
              dispatch(
                userActions.changeInputValue({
                  name: "dateOfBirth",
                  value: date,
                })
              );
            }}
          />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>DATA URODZENIA</Label>
        </Field>
        <Text>Adres</Text>
        <Field>
          <Input name="houseNumber" />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>NUMER DOMU/LOKALU</Label>
        </Field>
        <Field>
          <Input name="road" />
          <Label>ULICA</Label>
        </Field>

        <Field>
          <Input name="zipcode" />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>KOD POCZTOWY</Label>
        </Field>
        <Field>
          <Input name="place" />
          <Span type="highlight" />
          <Span type="bar" />
          <Label>MIEJSCOWOŚĆ</Label>
        </Field>
        <Button>Zapisz </Button>
      </Container>
    </Wrapper>
  );
}
export default Account;
