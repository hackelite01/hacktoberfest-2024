import React from "react";
import loading from "./loading.gif";

const Spinner = () => {
  return (
    <div className="text-center">
      <img
        className="my-4"
        src={loading}
        alt="loading"
        style={{ width: "2rem", height: "2rem" }}
      />
    </div>
  );
};

export default Spinner;
