
pragma solidity >=04.21 < 0.6.0;
contract Object{

  STRING public object;
  constructor(string memory message) publlic {
    object = message;

  }
  function getOriginalMessage() public view returns{
    return object;
  }
}