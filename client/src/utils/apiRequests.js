import axios from "axios";

export function getUserInfo(token) {
  if (token !== null) {
    const config = { headers: { Authorization: `Bearer ${token}` } };

    return axios
      .get("http://localhost:5198/api/user/userInfo", config)
      .then((response) => {
        console.log(response);
        return response.data;
      })
      .catch((err) => {
        console.log(err);
        throw err;
      });
  } else {
    return false;
  }
}
