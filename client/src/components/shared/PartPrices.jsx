const PartPrices = ({ prices }) => {
  var min = Math.min(...prices.map((item) => item.price));
  return (
    <div class="dropdown dropdown-right">
      <div tabindex="0" role="button" class="btn m-1">
        {prices !== undefined && prices.length !== 0 && min} zł
      </div>
      <ul
        tabindex="0"
        class="dropdown-content z-[1] menu p-2 shadow bg-base-100 rounded-box w-52"
      >
        {prices !== undefined &&
          prices.length !== 0 &&
          prices.map((price) => (
            <li>
              <p>
                <a href={price.link}>
                  {price.price} zł - {price.shopName}
                </a>
              </p>
            </li>
          ))}
      </ul>
    </div>
  );
};
export default PartPrices;
