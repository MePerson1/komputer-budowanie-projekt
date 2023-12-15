import { useLocation, Link } from "react-router-dom";

const Breadcrumbs = () => {
  const location = useLocation();

  let currentLink = "";
  const crumbs = location.pathname
    .split("/")
    .filter((crumb) => crumb !== "")
    .map((crumb) => {
      currentLink += `/${crumb}`;
      return (
        <li>
          <Link to={currentLink}>{crumb}</Link>
        </li>
      );
    });

  return (
    <div class="text-sm breadcrumbs">
      <ul>{crumbs}</ul>
    </div>
  );
};

export default Breadcrumbs;
