import React, { useState } from "react";
import "./SignIn.scss";
import { useSelector, useDispatch } from "react-redux";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import userAction from "../../redux/user/actions";
import {
  Button,
  Field,
  Formulee,
  Wrapper,
  Span,
  Label,
  Input,
} from "./components";

function SignIn() {
  const user = useSelector((state) => state.user.data);
  const dispatch = useDispatch();

  return (
    <Wrapper>
      <Formulee>
        <Field>
          <Input name="username" />
          <Span type="highlight"></Span>
          <Span type="bar"></Span>
          <Label>Username</Label>
        </Field>
        <Field>
          <Input name="password" type="password" />
          <Span type="highlight"></Span>
          <Span type="bar"></Span>
          <Label>Password</Label>
        </Field>
        <Field>
          <Input name="email" />
          <Span type="highlight"></Span>
          <Span type="bar"></Span>
          <Label>Email</Label>
        </Field>
        <Field>
          <Label>Data urodzenia</Label>
          <DatePicker
            selected={new Date(user.dateOfBirth)}
            onChange={(date) =>
              dispatch(
                userAction.changeInputValue({
                  name: "dateOfBirth",
                  value: date,
                })
              )
            }
          />
        </Field>
        <Field>
          <Button>Zarejestruj</Button>
        </Field>
      </Formulee>
    </Wrapper>
  );
}
export default SignIn;
