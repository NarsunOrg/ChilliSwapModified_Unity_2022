mergeInto(LibraryManager.library, {

  getWalletAddressUnity: function () {
    
	getWalletAddress();
  },

  getTokenUnity: function() {
    getToken();
  },

  getBalanceUnity: function (str) {
    balanceOf();
  },

    stakeUnity: function (str) {
	
    stake(Pointer_stringify(str));
  },
   getStakedBalanceUnity: function (str) {
    getStakedBalance(Pointer_stringify(str));
  },
   retrieveStakeUnity: function () {
    
	retrieveStake();
  },

  registerVisibilityChangeEvent: function () {
    document.addEventListener("visibilitychange", function () {
      SendMessage("MyObject", "OnVisibilityChange", document.visibilityState);
    });
    if (document.visibilityState != "visible")
      SendMessage("MyObject", "OnVisibilityChange", document.visibilityState);
  },

});