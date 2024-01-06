export const Pagination = ({ paginationInfo, handlePageChange }) => {
  const renderPageInputs = () => {
    const pageInputs = [];
    if (paginationInfo.TotalPages) {
      for (let i = 1; i <= paginationInfo.TotalPages; i++) {
        pageInputs.push(
          <input
            key={i}
            className="join-item btn"
            type="radio"
            name="options"
            aria-label={i}
            value={i}
            checked={i === paginationInfo.CurrentPage}
            onChange={() => handlePageChange(i)}
          />
        );
      }
    }
    return pageInputs;
  };
  return (
    <div class="join flex justify-center items-center">
      {paginationInfo && renderPageInputs()}
    </div>
  );
};
