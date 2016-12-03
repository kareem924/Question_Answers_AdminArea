var ObjectState = {
    Unchanged: 0,
    Added: 1,
    Modified: 2,
    Deleted: 3
};


var ChoicesMapping = {
    'ChoicesItems': {
        key: function (choiceItem) {
            return ko.utils.unwrapObservable(choiceItem.ChoiceId);
        },
        create: function (options) {
            return new ChoicesViewModel(options.data);
        }
    }
};
var dataConverter = function (key, value) {
    if (key === 'RowVersion' && Array.isArray(value)) {
        var str = String.fromCharCode.apply(null, value);
        return btoa(str);
    }

    return value;
};

ChoicesViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, ChoicesMapping, self);
    self.flagChoicesAsEdited = function () {
        if (self.ObjectState() !== ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }

        return true;
    }
   
};


QuestionViewModel = function (data) {
    var self = this;
    ko.mapping.fromJS(data, ChoicesMapping, self);

    self.save = function () {
        $.ajax({
            url: "/Questions/Save/",
            type: "POST",
            data: ko.toJSON(self),
            contentType: "application/json",
            success: function (data) {
                if (data.success) {
                   
                    if (data.QuestionViewModel != null)
                        ko.mapping.fromJS(data.QuestionViewModel, {}, self);

                    if (data.newLocation != null)
                        window.location = data.newLocation;
                } else {
                    $.notify({
                        // options
                        icon: 'glyphicon glyphicon-warning',
                        title: '',
                        message: data.message
                    }, {
                        type: "danger",

                        placement: {
                            from: "bottom ",
                            align: "right"
                        }
                    });
                }
            },
            error: function (XMLHttpRequest,data, textStatus, errorThrown) {
                if (XMLHttpRequest.status == 400) {
                 
                    $.notify({
                        // options
                        icon: 'glyphicon glyphicon-warning',
                        title: '',
                        message: XMLHttpRequest.responseText
                    }, {
                        type: "danger",

                        placement: {
                            from: "bottom ",
                            align: "right"
                        }
                    });
                }
                else if (data.message) {
                    $.notify({
                        // options
                        icon: 'glyphicon glyphicon-warning',
                        title: '',
                        message: data.message
                    }, {
                        type: "danger",

                        placement: {
                            from: "bottom ",
                            align: "right"
                        }
                    });
                }
                else {
                   
                    $.notify({
                        // options
                        icon: 'glyphicon glyphicon-warning',
                        title: '',
                        message: 'The web server had an error.'
                    }, {
                        type: "danger",

                        placement: {
                            from: "bottom ",
                            align: "right"
                        }
                    });
                }
            }
        });
    },

    self.flagQuestionAsEdited = function () {
        if (self.ObjectState() != ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }
        return true;
    },

     
    
    self.addChoice = function () {
        var choiceItem = new ChoicesViewModel({ ChoiceId: 0, ChoiceText: "", ChoiceLetter: "", IsSelected: false, ObjectState: ObjectState.Added });
        self.ChoicesItems.push(choiceItem);
    },
    self.deleteChoiceItem = function (choice) {
        self.ChoicesItems.remove(this);

        if (choice.ChoiceId() > 0 && self.ChoicesToDelete.indexOf(choice.ChoiceId()) === -1)
            self.ChoicesToDelete.push(choice.ChoiceId());
    };
};
$("form").validate({
    submitHandler: function () {
        QuestionViewModel.save();
    },

    rules: {
        QuestionText: {
            required: true
          
        },
        ChoiceText: {
            required: true
        },
        ChoiceLetter: {
            required: true
        }
        
    },

    messages: {
        QuestionText: {
            required: "You cannot create a Question unless you supply the Question Text.",
        },
        ChoiceText: {
            required: "Choice text is Required."
        },
        ChoiceLetter: {
            required: "Choice Letter is Required."
        }
        
    },

    tooltip_options: {
        QuestionText: {
            placement: 'right'
        },
        ChoiceText: {
            placement: 'right'
        },
        ChoiceLetter: {
            placement: 'right'
        }
    }
});

