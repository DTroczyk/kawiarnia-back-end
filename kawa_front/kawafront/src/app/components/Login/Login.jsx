import React from "react";
import "./Login.scss";

import {
  Button,
  Container,
  Field,
  Formulee,
  Input,
  Label,
  Span,
  Wrapper,
} from "./components";
export default function Login() {
  return (
    <Wrapper>
      <Container>
        <Formulee>
          <Field>
            <Input name="username" />
            <Span type="highlight" />
            <Span type="bar" />
            <Label>Login</Label>
          </Field>
          <Field>
            <Input name="password" type="password" />
            <Span type="highlight" />
            <Span type="bar" />
            <Label>Hasło</Label>
          </Field>

          <Field>
            <Button name="login">Zaloguj</Button>
          </Field>
          <Field>
            <Button name="signIn">Dołącz</Button>
          </Field>
        </Formulee>
      </Container>
    </Wrapper>
  );
}
