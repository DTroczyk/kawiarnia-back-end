function getFetchHeader(method, token,body) {
  let headers = new Headers();
  let requestOption = {};
  switch (method) {
    case "GET":
      headers.append("Authorization", `Bearer ${token}`);
      requestOption = {
        method,
        headers,
      };
      return requestOption;
    case "PUT":
      headers.append("Content-Type", `application/json`);
      headers.append("Authorization", `Bearer ${token}`);
      requestOption = {
        method,
        headers,
        body
      };
      return requestOption;
    case "POST":
      headers.append("Content-Type", `application/json`);
      headers.append("Authorization", `Bearer ${token}`);
      requestOption = {
        method,
        headers,
        body
      };
      return requestOption;
    case "DELETE":
      headers.append("Authorization", `Bearer ${token}`);
      requestOption = {
        method,
        headers,
      };
      return requestOption;
    default:
      break;
  }
  return;
}
export default getFetchHeader;
