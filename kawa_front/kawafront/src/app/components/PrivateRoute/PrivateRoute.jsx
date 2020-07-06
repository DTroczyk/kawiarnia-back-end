import React from "react";
import { useSelector } from "react-redux";
import { Route, Redirect } from "react-router-dom";
function PrivateRoute({ children, ...rest }) {
  const token = useSelector((state) => state.user.token);
  return (
    <Route
      {...rest}
      render={({ location }) =>
        token ? (
          children
        ) : (
          <Redirect
            to={{
              pathname: "/",
              state: { from: location },
            }}
          />
        )
      }
    />
  );
}
export default PrivateRoute;
