import axios from "axios";
//import dotenv from "dotenv";

const api_request = axios.create({
  baseURL: "http://localhost:8080/", 
  //baseURL: process.env.API_URL, //oczywiście nie działa *@() wie czemu
  timeout: 1000,
});

//Pewnie będziemy trzymali całego użytkownika ale póki co jest tylko token
api_request.interceptors.request.use(
    (config) => {
        // const token = localStorage.getItem("token");
        // if (token) 
        // {
        //     config.headers["x-access-token"] = token; 
        // }

        //dodaj naglowek z tokenem do kazdego zapytania o ile token istnieje
        const userDetails = localStorage.getItem("user");
        if (userDetails) 
        {
            const token = JSON.parse(userDetails).token;
            config.headers.Authorization = `Bearer ${token}`;
        }

        return config;
    },
  (error) => {
    return Promise.reject(error);
  }
);

export default api_request;
