mergeInto(LibraryManager.library, {

    GetJSON : function(string path, string objectName, string callback, string fallback) {

        var parsedPath = Pointer_stringify(path);
        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);

        try {
            firebase.database().ref(parsedPath).once('value').then(function(snapshot) {
                unityInstance.Module.SendMessage(objectName, callback, JSON.stringify(snapshot.val()));
            });
        } catch (error) {
                unityInstance.Module.SendMessage(objectName, fallback, "There was an error: " + error.message));
        }

        
    }

    
}); 