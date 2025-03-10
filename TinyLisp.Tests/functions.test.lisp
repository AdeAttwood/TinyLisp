
(defun some-global-function ()
  "This returns a string")

(deftest it-calls-a-global-function ()
  (assert-eq "This returns a string" (some-global-function)))

(deftest it-calls-a-local-function ()
  (defun some-local-function ()
    "This local function")

  (assert-eq "This local function" (some-local-function)))

(deftest it-evaluates-the-list-before-using-it-as-a-function-param ()
   (defun plus-one (a)
     (+ a 1))

   (assert-eq 10 (plus-one (+ 5 4))))
