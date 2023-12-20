import { Link } from "react-router-dom";

export const Logo = () => {
  return (
    <Link to="/" className="logo w-16 h-16">
      <img src="logo192.png" alt="logo" />
    </Link>
  );
};
