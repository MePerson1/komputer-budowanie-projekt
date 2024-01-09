import axios from "axios";

const url = "http://localhost:5198/api";

export function getTokenConfig(token) {
  const config = { headers: { Authorization: `Bearer ${token}` } };
  return config;
}

export async function getUserInfo(token) {
  if (token !== null) {
    const config = await getTokenConfig(token);

    try {
      const response = await axios.get(
        "http://localhost:5198/api/user/userInfo",
        config
      );
      localStorage.setItem("loggedUser", JSON.stringify(response.data));
      return response.data;
    } catch (err) {
      console.log(err);
      if (err.response && err.response.status === 401) {
        console.log("Unauthorized access!");
        localStorage.removeItem("token");
        localStorage.removeItem("loggedUser");
        window.location.reload();
      } else {
        console.log("Error occurred:", err);
        localStorage.removeItem("token");
        localStorage.removeItem("loggedUser");
        window.location.reload();
      }
    }
  } else {
    return false;
  }
}

export async function changeEmail(token, newEmail, currentPassword) {
  if (token !== null) {
    const config = getTokenConfig(token);
    return await axios
      .put(
        "http://localhost:5198/api/user/changeemail",
        { newEmail, currentPassword },
        config
      )
      .then((response) => {
        console.log(response);
        return response.data;
      })
      .catch((err) => {
        console.log(err);
      });
  } else {
    return false;
  }
}
