"use strict";
function FilterClass() {
    return { Cycle_ID: $("#Cycle_ID").data("kendoDropDownList").value() };
}
function FilterDivision() {
    return { Class_ID: $("#Class_ID").data("kendoDropDownList").value() };
}