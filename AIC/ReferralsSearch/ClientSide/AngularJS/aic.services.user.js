aic.services.user = aic.services.user || {};

aic.services.user.login = function (data, onSuccess) {
    aic.page.sendAjax("users/login", "POST", data, onSuccess);
}

aic.services.user.register = function (data, onSuccess, onError) {
    var url = "/api/users/Register"

    // step 2: Create our Package (C2D2SET)dd
    var settings = {
        /*C2D2SET*/
        cache: false, // <== usually set to FALSE will tell teh endpoint not to cache the page
        contentType: "application/json", // <== this is the format our content will be in

        dataType: "json", // <== format our data is in
        data: JSON.stringify(data), // <== our payload (an object, a simple object)

        success: onSuccess, // <== method will fire if the endpoint tells us transmission is successful
        error: onError, // <== method will fire if endpoint throws an error
        type: "POST", // <== the type of request (GET, POST, PUT, DELETE)
        xhrFields: {
            withCredentials: true
        }
    };

    // STEP 3: Send the pacakage to the endpoint

    $.ajax(url, settings);
} // end aic.page.sendAjax


aic.services.user.registerReferral = function (data, onSuccess, onError) {
    var url = "/api/users/RegisterReferral"

    // step 2: Create our Package (C2D2SET)dd
    var settings = {
        /*C2D2SET*/
        cache: false, // <== usually set to FALSE will tell teh endpoint not to cache the page
        contentType: "application/json", // <== this is the format our content will be in

        dataType: "json", // <== format our data is in
        data: JSON.stringify(data), // <== our payload (an object, a simple object)

        success: onSuccess, // <== method will fire if the endpoint tells us transmission is successful
        error: onError, // <== method will fire if endpoint throws an error
        type: "POST", // <== the type of request (GET, POST, PUT, DELETE)
        xhrFields: {
            withCredentials: true
        }
    };

    // STEP 3: Send the pacakage to the endpoint

    $.ajax(url, settings);
} // end aic.page.sendAjax


aic.services.user.resetRequest = function (data, onSuccess) {
    aic.page.sendAjax("users/resetpassword", "POST", data, onSuccess);
}

aic.services.user.resendEmailConfirmation = function (data, onSuccess) {
    aic.page.sendAjax("users/resendEmailConfirmation", "POST", data, onSuccess);
}

aic.services.user.resetPasswordConfirmation = function (data, onSuccess) {
    aic.page.sendAjax("users/ConfirmResetPassword", "POST", data, onSuccess);
}


aic.services.user.changePasswordConfirmation = function (data, onSuccess) {
    aic.page.sendAjax("users/ChangePassword", "POST", data, onSuccess);
}

aic.services.user.userSelectAll = function (onSuccess) {
    aic.page.sendAjax("users/all", "GET", null, onSuccess);
}

aic.services.user.userSelectPaginate = function (query, onSuccess) {
    aic.page.sendAjax("users/paginate/" + query, "GET", null, onSuccess);
}

aic.services.user.userSearchUser = function (query, personId, onSuccess) {
    aic.page.sendAjax("users/user/" + query + "/" + personId, "GET", null, onSuccess);
}
aic.services.user.userSearchEducation = function (query, personId, onSuccess) {
    aic.page.sendAjax("users/education/" + query + "/" + personId, "GET", null, onSuccess);
}
aic.services.user.userSearchCompany = function (query, personId, onSuccess) {
    aic.page.sendAjax("users/company/" + query + "/" + personId, "GET", null, onSuccess);
}
// for referral search
aic.services.user.getProfilesWSameCompany = function (personID, pageNum, pageSize, onSuccess) {
    aic.page.sendAjax("profile/ReferralSearch/" + personID + "/" + pageNum + "/" + pageSize, "GET", null, onSuccess);
} //aic.services.user.getProfilesWSameCompany

aic.services.user.requestReferral = function (data, onSuccess) {
    aic.page.sendAjax("referral", "PUT", data, onSuccess);
} //aic.services.user.requestReferral

aic.services.user.unreadMessages = function (onSuccess) {
    aic.page.sendAjax("users/unreadmessages", "GET", null, onSuccess);

}

aic.services.user.flagMessages = function (onSuccess) {
    aic.page.sendAjax("users/flagunread", "GET", null, onSuccess);

}

aic.services.user.sendReferralToken = function (data, onSuccess) {
    aic.page.sendAjax("users/referrertoken", "POST", data, onSuccess);
}