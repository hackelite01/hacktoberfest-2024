import React from "react";
import { Link } from "react-router-dom";

const NavBar = () => {
  return (
    <div>
      <nav
        className="navbar fixed-top navbar-expand-lg navbar-dark "
        style={{ backgroundColor: "#000" }}
      >
        <div className="container-fluid">
          <Link className="navbar-brand" to="/" style={{ fontSize: "1.5rem" }}>
            NewsPulse
          </Link>
          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
            style={{ marginLeft: "6rem" }}
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="container ">
            <div
              className="collapse navbar-collapse"
              id="navbarSupportedContent"
            >
              <ul className="navbar-nav me-auto mb-2 mb-lg-0 ">
                <li className="nav-item">
                  <Link
                    className="nav-link "
                    aria-current="page"
                    to="/"
                    style={{ color: "#2dd3c3", fontSize: "1.2rem" }}
                  >
                    Home
                  </Link>
                </li>

                <li className="nav-item">
                  <Link
                    className="nav-link"
                    to="/business"
                    style={{ color: "#2dd3c3", fontSize: "1.2rem" }}
                  >
                    {" "}
                    Business{" "}
                  </Link>
                </li>
                <li className="nav-item">
                  <Link
                    className="nav-link"
                    to="/entertainment"
                    style={{ color: "#2dd3c3", fontSize: "1.2rem" }}
                  >
                    {" "}
                    Entertainment{" "}
                  </Link>
                </li>
                <li className="nav-item">
                  <Link
                    className="nav-link"
                    to="/general"
                    style={{ color: "#2dd3c3", fontSize: "1.2rem" }}
                  >
                    {" "}
                    General{" "}
                  </Link>
                </li>
                <li className="nav-item">
                  <Link
                    className="nav-link"
                    to="/health"
                    style={{ color: "#2dd3c3", fontSize: "1.2rem" }}
                  >
                    {" "}
                    Health{" "}
                  </Link>
                </li>
                <li className="nav-item">
                  <Link
                    className="nav-link"
                    to="/science"
                    style={{ color: "#2dd3c3", fontSize: "1.2rem" }}
                  >
                    {" "}
                    Science{" "}
                  </Link>
                </li>
                <li className="nav-item">
                  <Link
                    className="nav-link"
                    to="/sports"
                    style={{ color: "#2dd3c3", fontSize: "1.2rem" }}
                  >
                    {" "}
                    Sports{" "}
                  </Link>
                </li>
                <li className="nav-item">
                  <Link
                    className="nav-link"
                    to="/technology"
                    style={{ color: "#2dd3c3", fontSize: "1.2rem" }}
                  >
                    {" "}
                    Technology{" "}
                  </Link>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </nav>
    </div>
  );
};

export default NavBar;
