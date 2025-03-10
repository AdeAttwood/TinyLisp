(deftest values-are-equal ()
  (assert (= 1 1) "Two numbers are equal")

  (assert (= true true) "true values are equal")
  (assert (= false false) "false values are equal"))

(deftest values-not-are-equal ()
  (assert (!= 1 2) "Two different numbers not are equal")
  (assert (!= true false) "true and false are not equal")
  (assert (!= 1 "1") "Numbers and strings are not equal"))

(deftest it-is-grater-than ()
  (assert (> 10 5) "Numbers are grater than")
  (assert (< 5 10) "Numbers are less than")

  (assert (>= 10 5) "Numbers are grater than or equal to")
  (assert (>= 10 10) "Numbers are grater than or equal to with equal values")
  (assert (<= 5 10) "Numbers are less than")
  (assert (<= 10 10) "Numbers are less than or equal to with equal values"))

(deftest if-statements ()
  (assert (if true true false) "I mean... this should work")
  (assert (if false false true) "And it returns the else if its false")
  (assert (if (= 10 10) true false) "Evaluates the condition")
  (assert (if true (= 10 10) false) "Evaluates return true value")
  (assert (if false false (= 10 10)) "Evaluates return false value")

  (assert (if true true (assert-eq true false)) "The assert-eq should not be evaluated"))

