# DNA.Patterns v1.0.1 
Released: 3/16/2014

## What is new ?
* Add strong name support
* Added CommandBase to support inject the Receiver object
* Added Command class to support undo and error handling 
* Added ICanUndo to support undoable command
* Added IErrorHandler to support command error handling.
* Support dynamic command
* Added feature **Invokers**
 + CommandStack
 + CommandTable
 + Macro
 + Transction
* Removed (You can use Macro to instead)
  + CommandBase
  + ParallelCommandManifestBase
  + SequenceCommandManifestBase
