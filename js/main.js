$(document).ready(function () {



    //SEARCH

    $("#search").on("input", function () {
        var searchTxt = $(this).val();
        var options = {};

        //if (searchTxt.length > 2) {
        $("table.listview tr td.title").each(function () {
            var _this = this;
            if ($(_this).text().toLowerCase().indexOf(searchTxt.toLowerCase()) < 0) {
                $(_this).parent("tr").hide();
            }
            else {
                $(_this).parent("tr").show();
            }
            $(_this).unmark({
                done: function () {
                    $(_this).mark(searchTxt, options);
                }
            });
        });
        //}
        //else {
        //    $("table.listview tr").show();
        //    $("table.listview tr td.title").each(function () {
        //        highlight($(this), searchTxt, false);
        //    });
        //}     
    });


    $('#nav').click(function () {
        $(this).toggleClass('open');
        $(".sidemenu").toggleClass("open");
    });



    //$("select").chosen({ no_results_text: "Oops, nothing found!", width: "100%" });


    var groups = {};
    $("select option[data-optiongroup]").each(function () {
        groups[$.trim($(this).attr("data-optiongroup"))] = true;
    });
    $.each(groups, function (c) {
        $("select option[data-optiongroup='" + c + "']").wrapAll('<optgroup label="' + c + '">');
    });

    $(".datepicker").datepicker(
        {
            dateFormat: "dd-mm-yy",
            showWeek: true,
            firstDay: 1,
            dayNamesMin: ["Sø", "Ma", "Ti", "On", "To", "Fr", "Lø"],
            monthNames: ["Januar", "Februar", "Marts", "April", "Maj", "Juni", "Juli", "August", "September", "Oktober", "November", "December"]
        }
    );


    $("#calTopRight").datepicker(
        {
            dateFormat: "dd-mm-yy",
            showWeek: true,
            firstDay: 1,
            dayNamesMin: ["Sø", "Ma", "Ti", "On", "To", "Fr", "Lø"],
            monthNames: ["Januar", "Februar", "Marts", "April", "Maj", "Juni", "Juli", "August", "September", "Oktober", "November", "December"]

        });
    //Main Menu
    $(".selected_case").on("click", function () {
        $(this).toggleClass("open");
        $(".dropdown_cases").toggle("slide", 300);
        return false;
    });

    $(".btn_open_menu").on("click", function () {
        $(".sidemenu").toggleClass("show");
        $(".dropdown_cases").hide();
    });



    $(".btn_open_buttons").on("click", function () {

        $(".topbuttons").toggleClass("show"); 

    });

    // Modal

    $(".btn_open_modal_create").on("click", function () {
        $(".modal.bg_overlay").show();
        $(".modal.create").show("fade");
        return false;
    });

    $(".btn_open_editbox").on("click", function () {
        $(".modal.bg_overlay").show();
        $(".modal.editbox").show("fade");
        return false;
    });


    $(".btn_open_modal_filter").on("click", function () {
        $(".modal.bg_overlay").show();
        $(".modal.filter").show("fade");
        return false;
    });

    $("ul.modal_tabs li").on("click", function () {
        $("ul.modal_tabs li").removeClass("selected");
        $(this).addClass("selected");

        $("div.tab").hide();
        $($("div.tab")[$(this).index()]).show();
    });


    $(".waive_add_material_ddl").on("change", function () {
        var materialid = $(".waive_add_material_ddl option:selected").val();
        $("[id$=hidMaterialID]").val(materialid);
    });
    $(".modal").draggable();



    var tabindex = location.href.substring(location.href.indexOf("#"));

    if (tabindex != null) {
        tabindex = tabindex.substring(tabindex.indexOf("-") + 1);
        console.log(tabindex);
        $(".tab").removeClass("selected");

        $($(".tabs-menu li")[tabindex]).addClass("selected");
        $($(".tab")[tabindex]).addClass("selected");
        $($(".topbuttons .buttons_item")[tabindex]).addClass("selected");
    }


    $(".tabs-menu li").on("click", function () {

        location.href = location.href.substring(0, location.href.indexOf("#")) + "#tabindex-" + $(this).index();

        $(".tabs-menu li").removeClass("selected");
        $(".topbuttons .buttons_item.selected").removeClass("selected");
        $(".tab").removeClass("selected");

        $(this).addClass("selected");
        $($(".tab")[$(this).index()]).addClass("selected");

        $($(".topbuttons .buttons_item")[$(this).index()]).addClass("selected");
    });

    $(".tabs-menu li a").on("click", function (e) {
        e.stopPropagation();
        $(".tabs-menu li").removeClass("selected");
        $(this).parent("li").addClass("selected");
        $(".listview, .emailwrap").html("<div class='lds-ellipsis'><div></div><div></div><div></div><div></div></div>");

    });

    var table = $('table.listview');

    $('th').each(function () {

            var th = $(this),
                thIndex = th.index(),
                inverse = false;

            th.click(function () {

                table.find('td').filter(function () {

                    return $(this).index() === thIndex;

                }).sort(function (a, b) {

                    return $.text([a]) > $.text([b]) ?
                        inverse ? -1 : 1
                        : inverse ? 1 : -1;

                }, function () {

                    // parentNode is the element we want to move
                    return this.parentNode;

                });

                inverse = !inverse;

            });

        });

});


$(function () {
    var dateFormat = "mm/dd/yy",
        from = $("[id$=txtFilterDateFrom]")
            .datepicker({
                showWeek: true,
                firstDay: 1
            })
            .on("change", function () {
                to.datepicker("option", "minDate", getDate(this));
            }),
        to = $("[id$=txtFilterDateTo]").datepicker({
            showWeek: true,
            firstDay: 1
        })
            .on("change", function () {
                from.datepicker("option", "maxDate", getDate(this));
            });

    function getDate(element) {
        var date;
        try {
            date = $.datepicker.parseDate(dateFormat, element.value);
        } catch (error) {
            date = null;
        }

        return date;
    }
});


//Button Clicks

function modalCreateSwitch(obj) {

    var btn = $(obj);

    if (btn.hasClass("fa-plus-circle")) {
        btn.parent(".label").next(".input").find("select").hide();
        btn.parent(".label").next(".input").find(".crete_new_input").show();
        btn.removeClass("fa-plus-circle");
        btn.addClass("fa-minus-circle");
    }
    else {
        btn.parent(".label").next(".input").find(".crete_new_input").val("").hide();
        btn.parent(".label").next(".input").find("select").show();
        btn.removeClass("fa-minus-circle");
        btn.addClass("fa-plus-circle");
    }

}

function openModalAddMaterialAmount(partid, materialid, ammount, type) {
    $("[id$=hidPartID]").val(partid);
    $("[id$=hidMaterialID]").val(materialid);
    switch (type) {
        case 1:
            $(".row.length").show();
            break;
        case 2:
            $(".row.length").show();
            $(".row.width").show();
            break;
        default:
            break;
    }
    $(".modal.bg_overlay").show();
    $(".modal.add_material_amount").show("fade");
    return false;
}


function createCase() {
    $(".modal.new.case").show("fade");
    $(".modal.bg_overlay").show();
}


function openModalEditMaterialAmount(partid, materialid, ammount, length, width) {
    $("[id$=hidPartID]").val(partid);
    $("[id$=hidMaterialID]").val(materialid);
    $("[id$=hidLength]").val(length);
    $("[id$=hidWidth]").val(width);

    $("[id$=txtEditAmount]").val(ammount);
    $(".modal.bg_overlay").show();
    $(".modal.edit_material_amount").show("fade");
}

function openModalAddWaiveMaterialAmount(materialid, ammount, type) {
    $("[id$=hidMaterialID]").val(materialid);

    switch (type) {
        case 1:
            $(".row.length").show();
            break;
        case 2:
            $(".row.length").show();
            $(".row.width").show();
            break;
        default:
            break;
    }
    $(".modal.bg_overlay").show();
    $(".modal.add_material_amount").show("fade");
    return false;
}


function openModalCreateMilestone() {
    $(".modal.bg_overlay").show();
    $(".modal.create.milestone").show("fade");
}

function openModalCreateActivity() {
    $(".modal.bg_overlay").show();
    $(".modal.create.activity").show("fade");
}

function openModalEditWaiveMaterialAmount(packageid, materialid, ammount, length, width) {
    $("[id$=hidPacakageID]").val(packageid);
    $("[id$=hidMaterialID]").val(materialid);
    $("[id$=hidLength]").val(length);
    $("[id$=hidWidth]").val(width);

    $("[id$=txtEditAmount]").val(ammount);
    $(".modal.bg_overlay").show();
    $(".modal.edit_material_amount").show("fade");
}

function removePackage(packageid, packageTitle) {
    $("[id$=hidPackageID]").val(packageid);
    $(".package_title").text(" " + packageTitle);
    $(".modal.bg_overlay").show();
    $(".modal.remove.remove_package").show("fade");
    return false;
}

function removeMaterial(materialid, materialTitle) {
    $("[id$=hidMaterialID]").val(materialid);
    $(".material_title").text(" " + materialTitle);
    $(".modal.bg_overlay").show();
    $(".modal.remove.remove_material").show("fade");
    return false;
}

function deleteMaterial(materialid, materialTitle) {
    $("[id$=hidMaterialID]").val(materialid);
    $(".material_title").text(" " + materialTitle);
    $(".modal.bg_overlay").show();
    $(".modal.delete.delete_material").show("fade");
    return false;
}
function deleteMilestone(milestoneid, milestoneTitle) {
    $("[id$=hidMilestoneID]").val(milestoneid);
    $(".milestone_title").text(" " + milestoneTitle);
    $(".modal.bg_overlay").show();
    $(".modal.delete.delete_milestone").show("fade");
    return false;
}


function deleteActivity(activityid, activityTitle) {
    $("[id$=hidActivityID]").val(activityid);
    $(".activity_title").text(" " + activityTitle);
    $(".modal.bg_overlay").show();
    $(".modal.delete.delete_activity").show("fade");
    return false;
}

function deletePart(partid, activityid, partTitle) {
    $("[id$=hidPartID]").val(partid);
    $("[id$=hidActivityID]").val(activityid);
    $(".part_title").text(" " + partTitle);
    $(".modal.bg_overlay").show();
    $(".modal.delete.delete_part").show("fade");
    return false;
}

function removePartfromPackage(partid, partTitle) {
    $("[id$=hidPartID]").val(partid);
    $(".part_title").text(" " + partTitle);
    $(".modal.bg_overlay").show();
    $(".modal.remove.remove_part").show("fade");
    return false;
}

function deletePackage(packageid, packageTitle) {
    $("[id$=hidPackageID]").val(packageid);
    $(".package_title").text(" " + packageTitle);
    $(".modal.bg_overlay").show();
    $(".modal.delete.delete_package").show("fade");
    return false;
}

function deleteContact(personid, personName) {
    $("[id$=hidContactID]").val(personid);
    $(".person_title").text(" " + personName);
    $(".modal.bg_overlay").show();
    $(".modal.delete.delete_contact").show("fade");
    return false;
}
function deleteCompany(companyid, companyName) {
    $("[id$=hidCompanyID]").val(companyid);
    $(".company_title").text(" " + companyName);
    $(".modal.bg_overlay").show();
    $(".modal.delete.delete_company").show("fade");
    return false;
}

function editPackageAmount(packageid, packageTitle, waiveAmount, waiveWeeks) {
    $("[id$=hidPackageID]").val(packageid);
    $(".package_title").text(" " + packageTitle);
    $("[id$=txtEditAmount]").val(waiveAmount);
    $("[id$=txtEditWeeks]").val(waiveWeeks);
    $(".modal.bg_overlay").show();
    $(".modal.edit.edit_package_amount").show("fade");
    return false;
}

function editPackageProdutionWeeks(packageid, packageTitle, waiveAmount, waiveWeeks) {
    $("[id$=hidPackageID]").val(packageid);
    $(".package_title").text(" " + packageTitle);
    $("[id$=txtEditAmount]").val(waiveAmount);
    $("[id$=txtEditWeeks]").val(waiveWeeks);
    $(".modal.bg_overlay").show();
    $(".modal.edit.edit_package_week").show("fade");
    return false;
}

function addWaivePackage() {
    $(".modal.bg_overlay").show();
    $(".modal.add_package").show("fade");
    return false;
}
function addWaiveMaterial() {
    $(".modal.bg_overlay").show();
    $(".waive_add_material").show("fade");
    return false;
}
function DeleteCase() {
    $(".case_title").text($("H1").text());
    $(".modal.bg_overlay").show();
    $(".modal.delete.delete_case").show("fade");
    return false;
}

function deleteWaive(waiveID, waiveTitle) {
    $(".waive_title").text(waiveTitle);
    $(".modal.bg_overlay").show();
    $("[id$=hidWaiveID]").val(waiveID);
    $(".modal.delete.delete_waive").show("fade");
    return false;
}

function EditWaiveAmounts() {
    $(".modal.bg_overlay").show();
    $(".modal.edit.waiveamount").show("fade");
    return false;
}

function deleteStructure(structureID, structureTitle) {
    $(".structure_title").text(structureTitle);
    $(".modal.bg_overlay").show();
    $("[id$=hidStructureID]").val(structureID);
    $(".modal.delete.delete_structure").show("fade");
    return false;
}

function getDate(element) {
    var date;
    try {
        date = $.datepicker.parseDate(dateFormat, element.value);
    } catch (error) {
        date = null;
    }

    return date;
}


function highlight(obj, txt, add) {

    var oldTxt = obj.text();
    if (add) {

        obj.html(obj.text().replace(txt, "<span class=\"highlight\">" + txt + "</span>"));
    }
    else {
        obj.html(obj.text().replace("<span class=\"highlight\">", "").replace("</span >", ""));
    }

}



function highlight_words(keywords, element) {
    if (keywords) {
        var textNodes;
        keywords = keywords.replace(/\W/g, '');
        var str = keywords.split(" ");
        $(str).each(function () {
            var term = this;
            var textNodes = $(element).contents().filter(function () { return this.nodeType === 3 });
            textNodes.each(function () {
                var content = $(this).text();
                var regex = new RegExp(term, "gi");
                content = content.replace(regex, '<span class="highlight">' + term + '</span>');
                $(this).replaceWith(content);
            });
        });
    }
}





