aic.services.workhistory = aic.services.workhistory || {};

aic.services.workhistory.get = function (onSuccess) {
    aic.page.sendAjax("workhistory", "GET", null, onSuccess);
}; //aic.services.workhistory.get

aic.services.workhistory.getById = function (id, onSuccess) {
    aic.page.sendAjax("workhistory/" + id, "GET", null, onSuccess);
}; //aic.services.workhistory.getById

aic.services.workhistory.put = function (id, data, onSuccess) {
    aic.page.sendAjax("workhistory/" + id, "PUT", data, onSuccess);
}; //aic.services.workhistory.put

aic.services.workhistory.post = function (data, onSuccess) {
    aic.page.sendAjax("workhistory", "POST", data, onSuccess);
}; //aic.services.workhistory.post

aic.services.workhistory.delete = function (id, onSuccess) {
    aic.page.sendAjax("workhistory/" + id, "DELETE", null, onSuccess);
}; //aic.services.workhistory.delete
