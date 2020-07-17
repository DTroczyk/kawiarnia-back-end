import React from "react";
import Login from "./app/components/Login/Login";
import "./App.css";
import SignIn from "./app/components/SignIn/SignIn";
import PrivateRoute from "./app/components/PrivateRoute/PrivateRoute";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Panel from "./app/components/Panel/Panel";
function App() {
  return (
    <Router>
      <Switch>
        <Route exact path="/">
          <Login />
        </Route>
        <Route path="/signin">
          <SignIn />
        </Route>
        <PrivateRoute path="/panel">
          <Panel />
        </PrivateRoute>
      </Switch>
    </Router>
  );
}

export default App;
