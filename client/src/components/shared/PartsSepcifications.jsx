export const PartsSpecifications = ({ specifications }) => {
  return (
    <table className="table table-auto table-sm ">
      <tbody className="bg-base-300">
        {specifications &&
          specifications.map((specification) => (
            <tr className="">
              <td>{specification.name}</td>
              <td>{specification.value}</td>
            </tr>
          ))}
      </tbody>
    </table>
  );
};
