import axios from "axios";

export const mainUrl = "http://localhost:5198/api";

export function getTokenConfig(token) {
  const config = { headers: { Authorization: `Bearer ${token}` } };
  return config;
}

export async function getUserInfo(token) {
  if (token !== null) {
    const config = await getTokenConfig(token);

    try {
      const response = await axios.get(`${mainUrl}/user/userInfo`, config);
      localStorage.setItem("loggedUser", JSON.stringify(response.data));
      return response.data;
    } catch (err) {
      console.log(err);
      if (err.response && err.response.status === 401) {
        console.log("Unauthorized access!");
        localStorage.removeItem("token");
        localStorage.removeItem("loggedUser");
        localStorage.removeItem("localEditedConfiugration");
        window.location.reload();
      } else {
        console.log("Error occurred:", err);
        localStorage.removeItem("token");
        localStorage.removeItem("loggedUser");
        localStorage.removeItem("localEditedConfiugration");
        window.location.reload();
      }
    }
  } else {
    return false;
  }
}
