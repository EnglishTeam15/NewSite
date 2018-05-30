$(document).ready(function () {
	$('#chekall').click(function () {
		if ($(this).is(':checked')) {
			$('table input:checkbox').prop('checked', true);
		} else {
			$('table input:checkbox').prop('checked', false);
		}
	});

	$('.chkclass').click(function () {
		var getchkid = $(this).attr('id');
		var isChecked = $('#' + getchkid).is(':checked');
		if ($('#' + getchkid).is(':checked') == true) {

			$('#chk' + $(this).val()).css("background", "#181f3770");

			$('#td' + $(this).val()).css("color", "#fff");
			$('#td' + $(this).val()).css("background-color", "#181f3770");
			$('#d' + $(this).val()).css("color", "white");
			$('#d' + $(this).val()).css("background-color", "#181f3770");
			$('#photo' + $(this).val()).css("background", "#181f3770");
		}
		else {
			$('#chk' + $(this).val()).css("background", "#fff");
			$('#td' + $(this).val()).css("color", "black");
			$('#td' + $(this).val()).css("background-color", "#fff");
			$('#d' + $(this).val()).css("color", "black");
			$('#d' + $(this).val()).css("background-color", "#fff");
			$('#photo' + $(this).val()).css("background", "#fff");
		}
	});
	$('#bttn_Click').click(function () {
		var wordlist = null;
		wordlist = [];
		$('tbody input:checkbox:checked').each(function () {
			wordlist.push($(this).attr('value'));
		});
		var count = wordlist.length;
		if (count == 0) {
			$('#errormesage').text("Выберите минимум 5 слов для изучения");
		}
		else if (count > 0 & count < 5) {
			$('#errormesage').text("Выбрано " + count + ", выбериеще еще минимум " + (5 - count) + " слов для изучения");
		}
		else {
			$('#errormesage').text("Выбрано " + count + " слов");
			$.ajax({
				type: "post",
				url: "/Main/ChoiceWord",
				data: { id: wordlist },
				datatype: "json",
				success: function (data) {
					var selectedIds;
					for (var i = 0; i < data.success.length; i++) {
						if (selectedIds != undefined) {
							selectedIds = selectedIds +"["+i+"]" + data.success[i];
						}
						else {
							selectedIds = data.success[i];
						}
					}
					$('#star-learn').css("display", "block");
					//$('#bttn_Click').css("display", "none");
					alert('You have Selected Student Ids- ' + selectedIds);
				},
				error: function (data) {
					alert('error ' + data.error);
				}
			});
		}
	});
});