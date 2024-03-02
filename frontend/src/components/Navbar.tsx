import React from "react";
import { useNavigate } from "react-router-dom";
function Navbar() {
  const redirect = useNavigate()
  return (
    <div className="flex justify-between p-3 px-5 items-center ">
      <div>
        <p className="text-4xl">Notes</p>
      </div>
      <div className="flex gap-3">
        <button  className="text-[18px]" onClick={()=>redirect("/")}>
          Home
        </button>
        <button className="text-[18px]" onClick={()=>redirect("/about")}>
          About
        </button>
        <button  className="text-[18px]" onClick={()=>redirect("/contact")}>
          Contact Us
        </button>
        <button  className="text-[18px]" onClick={()=>redirect("/auth/register")}>
          Sign Up
        </button>
        <button className="text-[18px]" onClick={()=>redirect("/auth/login")}>
          Login
        </button>
      </div>
    </div>
  );
}

export default Navbar;
