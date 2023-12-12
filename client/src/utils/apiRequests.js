import api_request from "./request";


export const register = async (data) => {
    try {
        return await api_request.post(`/authController/register`, data);
        } catch (exception) {
        return {
            error: true,
            exception,
    };
    }
};
  
export const login = async (data) => {
    try {
        return await api_request.post(`/authController/login`, data);
    } catch (exception) {
        return {
            error: true,
            exception,
    };
    }
};