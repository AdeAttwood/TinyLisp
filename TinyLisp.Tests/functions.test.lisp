
(defun some-global-function ()
  "This returns a string")

(deftest it-calls-a-global-function ()
  (assert-eq "This returns a string" (some-global-function)))

(deftest it-calls-a-local-function ()
  (defun some-local-function ()
    "This local function")

  (assert-eq "This local function" (some-local-function)))
