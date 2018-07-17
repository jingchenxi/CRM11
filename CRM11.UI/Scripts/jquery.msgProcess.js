(function (jqObj) {
	jqObj.extend(jqObj, {
		msgProcess: function (jsonText,funcOK,funcNoOk) {
			var msgObj = null;

			if (typeof(jsonText) == "string") {
				try {
					msgObj = eval(jsonText);

				} catch (e) {
					try {
						msgObj = eval("(" + jsonText + ")");

					} catch (e1) {
						alert("格式错误，必须为Json字符串");
					}
				}
			}else
			{
				if (jsonText.Status) {
					msgObj = jsonText;
				}

			}

			function doesMsgBoxExsit() {
				return $.msgBoxObj && $.msgBoxObj.showOK && $.msgBoxObj.showOK instanceof Function;

			}

			function invokeFunc(func) {
				if (func && func instanceof Function) {
					func(msgObj);
				}else
				{
					if (typeof (msgObj.BackUrl) == "string" && msgObj.BackUrl) {
						if (window.top == window)
							window.location = msgObj.BackUrl;
						else
							window.top.location = msgObj.BackUrl;

					}
				}

			}


			switch (msgObj.Status) {
				case 1:
					if (doesMsgBoxExsit()) {
						$.msgBoxObj.showOK(msgObj.Message, function () { invokeFunc(funcOK); });
					}
					break;
				case 2:
					if (doesMsgBoxExsit()) {
						$.msgBoxObj.showFailed(msgObj.Message, function () { invokeFunc(funcNoOk); });
					}
					break;

				case 3:
					if (doesMsgBoxExsit()) {
						$.msgBoxObj.showInfo(msgObj.Message, function () { invokeFunc(); });
					}
					break;
				case 4:
					if (doesMsgBoxExsit()) {
						$.msgBoxObj.showInfo(msgObj.Message, function () { invokeFunc(); });
					}
					break;

				case 5:
					if (doesMsgBoxExsit()) {
						$.msgBoxObj.showFailed(msgObj.Message, function () { alert(msgObj.Message); });
					}
			}



		}



	});

})(window.$);