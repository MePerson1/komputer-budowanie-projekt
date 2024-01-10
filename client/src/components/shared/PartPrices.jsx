import { useState } from "react";

const PartPrices = ({ prices }) => {
  if (!prices || prices.length === 0) {
    return null;
  }

  prices.sort((a, b) => a.price - b.price);
  const minPrice = Math.min(...prices.map((item) => item.price));

  return (
    <div class="dropdown dropdown-right">
      <div tabIndex="0" role="button" class="btn m-1">
        <div className="">{minPrice} zł</div>
        <img
          width="20"
          height="20"
          src="https://img.icons8.com/material-sharp/24/forward.png"
          alt="forward"
          className="invert"
        />
      </div>
      <ul
        tabIndex="0"
        class="dropdown-content z-[1] menu p-2 shadow bg-base-100 rounded-box w-80 border border-info"
      >
        {prices.map((price) => (
          <li key={price.id}>
            <div>
              <a href={price.link}>
                {price.price === minPrice ? (
                  <div className="badge badge-secondary badge-outline">
                    {price.price} zł Najniższa cena
                  </div>
                ) : (
                  <div className="badge badge-outline">{price.price} zł</div>
                )}
                {` : ${price.shopName} `}
              </a>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default PartPrices;
