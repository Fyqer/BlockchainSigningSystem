// SPDX-License-Identifier: MIT
pragma solidity >=0.4.21 <0.9.0;
contract Object{
  string public object
  constructor(string memory message) public {
    object = message;

  }
  function getOriginalMessage() public view returns(string memory temp){
    return object;
  }
}
